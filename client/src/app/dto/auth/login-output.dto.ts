export interface LoginOutputDTO {
  id: number;
  email: string;
  name: string;
  token: string;

  address: {
    street: string;
    addressNumber: string;
    city: string;
    state: string;
    zipCode: string;
  }
}