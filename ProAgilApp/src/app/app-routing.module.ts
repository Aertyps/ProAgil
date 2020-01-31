import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EventosComponent } from './eventos/eventos.component';
import { PalestranteComponent } from './palestrante/palestrante.component';
import { DashbordComponent } from './dashbord/dashbord.component';
import { ContatosComponent } from './contatos/contatos.component';


const routes: Routes = [
  {path: 'eventos', component: EventosComponent},
  {path: 'palestrantes', component: PalestranteComponent},
  {path: 'dashbord', component: DashbordComponent},
  {path: 'contatos', component: ContatosComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
