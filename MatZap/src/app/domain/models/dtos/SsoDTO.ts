import { User } from 'src/app/domain/models/user.model';

export class SsoDTO {

  access_token: string = '';
  me: User = new User();

}
