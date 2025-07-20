export interface UserModel {
  id: number;
  name: string;
  email: string;
  
  address: {
    street: string;
    city: string;
    state: string;
    zipCode: string;
    addressNumber: string;
  }
}