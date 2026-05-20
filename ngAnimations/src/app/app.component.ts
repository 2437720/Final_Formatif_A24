import { transition, trigger, useAnimation } from '@angular/animations';
import { Component } from '@angular/core';
import { bounce, shakeX, tada } from 'ng-animate';

const ShakeAnimationTime = 2;
const BounceAnimationTime = 4;
const TadaAnimationTime = 3;

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    animations: [
      trigger('shake', 
        [transition(':increment', useAnimation(shakeX, {
          params: { timing: 2}
        }))
      ]),
      trigger('bounce', [transition(':increment', useAnimation(bounce, {params: { timing: BounceAnimationTime}}))]),
      trigger('tada', [transition(':increment', useAnimation(tada, {params: {timing: TadaAnimationTime}}))])
    ],
    styleUrls: ['./app.component.css'],
    standalone: true
})
export class AppComponent {
  title = 'ngAnimations';

  ng_shake = 0;
  ng_bounce = 0;
  ng_tada = 0;

  constructor() {
  }


  AnimerUneFois(){
    console.log("Animer une fois");
    this.ng_shake++;
    
    setTimeout(() => {
      this.ng_bounce++;
    }, ShakeAnimationTime * 1000);
    setTimeout(() => {      this.ng_tada++;
    }, TadaAnimationTime * 1000);
        
  
  }


}
