import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { RegiaoService } from 'src/app/modules/regiao/regiao.service';
import * as XLSX from 'xlsx';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-regiao',
  templateUrl: './regiao.component.html',
  styleUrls: ['./regiao.component.scss']
})

export class RegiaoComponent implements OnInit {
  regiaoForm: FormGroup;
  cidadeControl: FormControl = new FormControl('Selecione');
  regioes$: Observable<any[]>; 
  cidadesSelecionadas: { id: string, nome: string }[] = [];
  cidadesDisponiveis: { id: string, nome: string }[] = [];
  isEditing = false;
  mostrarFormulario = false;
  regioes: any[] = [];

  constructor(
    private fb: FormBuilder,
    private regiaoService: RegiaoService,
    private router: Router
  ) {}

  ngOnInit() {
    this.regiaoForm = this.fb.group({
      nome: ['', Validators.required],
      id: [null],
      isActive: [null]
    });

    this.regioes$ = this.regiaoService.listarRegioes();

    this.regioes$.subscribe((data) => {
      this.regioes = data;  
    });

    this.carregarCidades();
  }

  carregarCidades() {
    this.regiaoService.listarCidades().subscribe({
      next: (cidades) => {
        this.cidadesDisponiveis = cidades.map(cidade => ({
          id: cidade.id,
          nome: cidade.nome
        }));
      },
      error: () => alert('Erro ao carregar cidades')
    });
  }

  inativarRegiao(id: number) {
    const regiao = this.regioes.find(r => r.id === id);
    if (!regiao) return;
  
    regiao.isActive = false;
    this.regiaoService.atualizarRegiao(regiao).subscribe({
      next: () => {
        this.regioes$ = this.regiaoService.listarRegioes();
        this.regioes$.subscribe((data) => {
          this.regioes = data;  
        });
        this.carregarCidades();
      },
      error: () => alert('Erro ao inativar região')
    });
  }
  
  ativarRegiao(id: number) {
    const regiao = this.regioes.find(r => r.id === id);
    if (!regiao) return;
  
    regiao.isActive = true;
  
    this.regiaoService.atualizarRegiao(regiao).subscribe({
      next: () => {
        this.regioes$ = this.regiaoService.listarRegioes();
      },
      error: () => alert('Erro ao ativar região')
    });
  }

  salvarRegiao() {
    const regiao = {
      id: this.isEditing ? this.regiaoForm.value.id : null,
      nome: this.regiaoForm.value.nome,
      isActive: true,
      cidades: [...this.cidadesSelecionadas]
    };

    const exits = this.regioes.some(r => r.nome === regiao.nome); 
    if (exits && !this.isEditing) {
      alert('Não é possível salvar uma região com o mesmo nome de outra existente.');
      return;
    }
    const operacao = this.isEditing
      ? this.regiaoService.atualizarRegiao(regiao)
      : this.regiaoService.criarRegiao(regiao);
  
    operacao.subscribe({
      next: () => {
        this.regioes$ = this.regiaoService.listarRegioes();
        this.regioes$.subscribe((data) => {
          this.regioes = data;  
        });
        this.carregarCidades();
        this.mostrarFormulario = false;
      },
      error: () => alert('Erro ao salvar uma região')
    });
  }

  editarRegiao(id: number) {    
    const regiao = this.regioes.find(r => r.id === id); 
    if (!regiao) return;
  
    this.isEditing = true;
    this.mostrarFormulario = true;
    
    this.regiaoForm.patchValue({
      nome: regiao.nome,
      id: regiao.id,
      isActive: regiao.isActive
    });

    this.cidadesSelecionadas = [...regiao.cidades];    
    this.cidadeControl.setValue('Selecione');
  } 

  adicionarCidade() {
    const cidade = this.cidadeControl.value;
    const exists = this.cidadesSelecionadas.some(s => s.nome === cidade.nome);    
    if(!exists){      
      if (cidade && cidade !== 'Selecione' && !this.cidadesSelecionadas.includes(cidade)) {
        this.cidadesSelecionadas.push(cidade);
      }
      this.cidadeControl.setValue('Selecione');
    }
    else{
      alert('Cidade já adicionada');
    }
  }

  removerCidade(cidade: { nome: string, id: string }) {
    this.cidadesSelecionadas = this.cidadesSelecionadas.filter(c => c.nome !== cidade.nome);
  }

  cancelarEdicao() {
    this.resetForm();
  }

  private resetForm() {
    this.regiaoForm.reset();
    this.cidadeControl.setValue('Selecione');
    this.cidadesSelecionadas = [];
    this.isEditing = false;
    this.mostrarFormulario = false;
  }

  toggleFormulario() {
    this.cidadeControl.setValue('Selecione');
    this.regiaoForm = this.fb.group({
      nome: ['', Validators.required],
      id: [null],
      isActive: [null]
    });
    this.cidadesSelecionadas = [];
    this.isEditing = false;
    this.mostrarFormulario = !this.mostrarFormulario;   
  }

exportarParaExcel(): void {
  this.regiaoService.listarRegioes().subscribe((regioes) => {
    const regioesFormatadas = regioes.map((regiao: any) => {
      return {
        'Nome': regiao.nome,
        'Ativo': regiao.isActive ? 'Sim' : 'Não',
        'Cidades': regiao.cidades.map((cidade: any) => cidade.nome).join(', '),
      };
    });

    const worksheet = XLSX.utils.json_to_sheet(regioesFormatadas);
    const workbook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(workbook, worksheet, 'Regiões');

    const excelBuffer: any = XLSX.write(workbook, {
      bookType: 'xlsx',
      type: 'array',
    });

    const blob = new Blob([excelBuffer], { type: 'application/octet-stream' });
    saveAs(blob, 'regioes.xlsx');
  });
}

}
