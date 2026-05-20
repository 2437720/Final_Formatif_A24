import { transition, trigger, useAnimation } from '@angular/animations';
import { Component } from '@angular/core';
import { bounce, shakeX, tada } from 'ng-animate';
import { delay } from 'rxjs';

const ShakeAnimationTime = 2;
const BounceAnimationTime = 4;
const TadaAnimationTime = 3;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  animations: [
    trigger('shake',
      [transition(':increment', useAnimation(shakeX, {
        params: { timing: ShakeAnimationTime }
      }))
      ]),
    trigger('bounce', [transition(':increment', useAnimation(bounce, { params: { timing: BounceAnimationTime } }))]),
    trigger('tada', [transition(':increment', useAnimation(tada, { params: { timing: TadaAnimationTime } }))])
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


  AnimerUneFois(boucle?: boolean) {
    console.log("Animer une fois");
    this.ng_shake++;



    setTimeout(() => {
      this.ng_bounce++;

      setTimeout(() => {
        this.ng_tada++;
      }, TadaAnimationTime * 999);

    }, ShakeAnimationTime * 1000);


    if(boucle){
      this.animerEnBoucle(true);
    }

  }

  animerEnBoucle(boucle: boolean) {
    let timing = ShakeAnimationTime + BounceAnimationTime + TadaAnimationTime;

      this.AnimerUneFois();
      setTimeout(() => {
        this.AnimerUneFois(true);

      }, timing * 1000);
  


  }


}
