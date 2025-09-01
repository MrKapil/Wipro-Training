import { Component, OnInit } from '@angular/core';
import { DataService } from '../data-service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-post-component',
  imports: [CommonModule],
  template: 
  `<ul>
    <li *ngFor="let post of posts">{{ post.title }}
    </li>
</ul>`
})

export class PostComponent implements OnInit{
  posts: any[] = [];

  constructor(private DataService: DataService){}

  ngOnInit() {
      this.DataService.getPosts().subscribe((data) => {
       this.posts = data;
      });
  }

}
