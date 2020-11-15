import { Endereco } from './endereco';

export interface Cliente {
  cnpj: string;
  nome: string;
  endereco: Endereco;
  ativo: boolean;
}
