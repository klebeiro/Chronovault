export interface ApiLoginOutputDTO {
  token: string;
  user: {
    id: number;
    email: string;
    name: string;
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
  };
}
