﻿using System;
namespace TodoApp.API.Models
{
	public class Todo
	{
		public Guid Id { get; set; }
		public string Description { get; set; }
		public DateTime CreatedDate { get; set; }
		public bool IsCompleted { get; set; }
		public DateTime? CompletedDate { get; set; }
		public bool isDeleted { get; set; }
		public DateTime? DeletedDate { get; set; }
		
	}
}

