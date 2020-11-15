import { Endereco } from './endereco';

export interface Motorista {

  cpf: string;
  cnh: string;
  email: string;
  endereco: Endereco;
  ativo: boolean;

}
