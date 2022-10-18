import { Injectable } from "@angular/core";
import { User } from "src/models";
import { ApiService } from "src/services";
import { environment } from "src/environments/environment";

@Injectable({ providedIn: "root" })
export class UserService {
  constructor(private apiService: ApiService) {}
  get = async (): Promise<User[]> => await this.apiService.get(`${environment.identityApi}/user`);
  post = async (model: User): Promise<User[]> => await this.apiService.post(`${environment.identityApi}/user`, model);
  put = async (model: User): Promise<User[]> => await this.apiService.put(`${environment.identityApi}/user`, model);
  delete = async (id: any): Promise<void> => await this.apiService.delete(`${environment.identityApi}/user/id/${id}`);
}
