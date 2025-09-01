import { Component, signal,OnInit, OnChanges, SimpleChanges } from '@angular/core';
@Component({
  selector: 'app-root',
  imports: [],
  templateUrl: './app.html',
  styleUrl: './app.css'
})

export class App implements OnChanges {
    title = 'onchanges';
    data: string = 'Initial Data';
    ngOnChanges(changes:SimpleChanges): void
     {
        if (changes ['data']) {
            console.log('Previous:', changes['data'].previousValue);
            console.log('Current:', changes['data'].currentValue);
        }
    }
    changeData() {
        this.data = 'Updated Data';
    }
}



/*
  "<div><h1>{{data}}</h1><h1>{{subtitle}}</h1></div>"
  ,
  styles:
  [
    `div{
        color: green;
    }`
  ]
})
export class App {
  protected readonly title = signal('LifeCycleHooks');


  //ngOnInit():-

  title1: string;
  subtitle: string = "";
  data: string = "";

  constructor() {
    this.title1 = "welcome to wipro";
  }

  ngOnInit() {
    this.data = "Welcome t Wipro";
    this.subtitle = "from ngOnInit";
  }

//ngOnChange():-



}*/