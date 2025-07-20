export interface RegisterInputDTO {
  name: string;
  email: string;
  password: string;
  confirmPassword: string;
  address: {
    street: string;
    addressNumber: string;
    city: string;
    state: string;
    zipCode: string;
  };
}
