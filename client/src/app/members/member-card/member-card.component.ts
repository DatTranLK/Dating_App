import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { Member } from 'src/app/_models/member';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css'],
  // encapsulation: ViewEncapsulation.None // hidden all ng attribute (angular css encapsulation) has appear in the tag
})
export class MemberCardComponent implements OnInit{

  @Input() member: Member | undefined;


  constructor() { }

  ngOnInit(): void {
  }

}
