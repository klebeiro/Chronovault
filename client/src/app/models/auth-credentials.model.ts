import { UserModel } from "./user.model";

export interface AuthCredentialsModel {
  user: UserModel;
  token: string;
}