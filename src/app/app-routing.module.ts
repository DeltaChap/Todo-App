import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DeletedTodoComponent } from './Components/deleted-todo/deleted-todo.component';
import { TodosComponent } from './Components/todos/todos.component';

const routes: Routes = [
  {
    path: '',
    component: TodosComponent
  },
  {
    path: 'todos',
    component: TodosComponent
  },
  {
    path: 'deleted-todo',
    component: DeletedTodoComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
