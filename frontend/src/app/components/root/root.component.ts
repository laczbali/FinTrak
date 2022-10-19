import { animate, group, query, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { NavigationEnd, NavigationStart, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './root.component.html',
  styleUrls: ['./root.component.scss'],
  "animations": [
    trigger('routeAnimation', [
      transition('* <=> *', [
        style({ position: 'relative' }),

        query(':enter, :leave', [
          style({
            position: 'absolute',
            top: 0,
            left: 0,
            width: '100%'
          })
        ], {optional: true}),

        query(':enter', [
          style({ left: '100%' })
        ], { optional: true }),

        group([
          query(':leave', [
            animate('200ms ease-out', style({ left: '-100%', opacity: 0 }))
          ], { optional: true }),
          
          query(':enter', [
            animate('300ms ease-out', style({ left: '0%' }))
          ], { optional: true })
        ])
      ])
    ])
  ]
})
export class RootComponent implements OnInit {

  constructor(private router: Router) {
    this.router.events.subscribe((val) => {
      if (val instanceof NavigationEnd) {
        this.currentRoute = val.url;
      }
    })
  }

  public currentRoute: string = '';

  ngOnInit() {
  }

  getRouteAnimationState(): string {
    return '';
  }

}
