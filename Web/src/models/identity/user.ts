export interface User {
  id?: string;
  email?: string;
  twoFactorEnabled?: boolean;
  roles?: string;
  clients?: string;
  landingPage?: string;
}
