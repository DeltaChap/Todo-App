import { Component, OnInit } from '@angular/core';
import { Todo } from '../../Models/todo.model';
import { TodoService } from '../../Services/todo.service';

@Component({
  selector: 'app-deleted-todo',
  templateUrl: './deleted-todo.component.html',
  styleUrl: './deleted-todo.component.css'
})
export class DeletedTodoComponent implements OnInit {
  todos: Todo[] = [];

  constructor(private todoService: TodoService) {}

  ngOnInit(): void {
    this.getAllDeletedTodos();
  }

  getAllDeletedTodos() {
    this.todoService.getAllDeletedTodos()
      .subscribe({
        next: (response) => {
          this.todos = response;
        }
      });
  }

  undoDeleteTodo(id: string, todo: Todo){
    this.todoService.undoDeleteTodo(id, todo)
    .subscribe({
      next: (response) => {
        this.getAllDeletedTodos();
      }
    });
  }

}
