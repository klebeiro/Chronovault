export interface ApiRegisterInputDTO {
  email: string;
  name: string;
  password: string;
  passwordConfirmation: string;
  phone: string;
  cpf: string;
  birthDate: string;
  address: {
    street: string;
    number: string;
    city: string;
    zipCode: string;
    state: string;
    country: string;
    complement: string | null;
  };
}
