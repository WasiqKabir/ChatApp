import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { ChatPageComponent } from './chat-page/chat-page.component';
import { AdminComponent } from './admin/admin.component';

const routes: Routes = [
  { path: '', component: LoginComponent }, // Default route to login page
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: SignupComponent },
  { path: 'chat', component: ChatPageComponent },
  { path: 'admin', component: AdminComponent}
];

export const AppRoutingRoutes = RouterModule.forRoot(routes);
