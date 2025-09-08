import { Component} from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';

// Shared
import { NavbarComponent } from '@shared/components/navbar/navbar';
import { FooterComponent } from '@shared/components/footer/footer';
import { ToastComponent } from '@shared/components/toast/toast/toast';
import { LoadingComponent } from '@shared/components/loading/loading/loading';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    NavbarComponent,
    FooterComponent,
    LoadingComponent,
    ToastComponent

  ],
  templateUrl: './app.html',
  styleUrls: ['./app.css'],
  
})
export class AppComponent { }
