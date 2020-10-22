import { Component, OnInit, ViewChild } from '@angular/core';
import {GlossaryTermsService} from '../services/glossary-terms.service';
import { GlossaryTerm } from '../models/GlossaryTerm';
import { MatTable } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from '../dialog/dialog.component';

@Component({
  selector: 'app-term-definition-display',
  templateUrl: './term-definition-display.component.html',
  styleUrls: ['./term-definition-display.component.css']
})
export class TermDefinitionDisplayComponent implements OnInit {
  glossaryTerms: GlossaryTerm[] = [];
  displayedColumns: string[] = ['term', 'definition', 'action'];
  @ViewChild(MatTable, {static: true}) table: MatTable<any>;

  constructor(public dialog: MatDialog, private glossaryTermsService: GlossaryTermsService) { }

  ngOnInit(): void {
    this.getGlossaryTerms();
  }
  private getGlossaryTerms() {
    this.glossaryTermsService.getGlossaryTerms().subscribe((res) => {
      this.glossaryTerms = res;
    });
  }

  openDialog(action,glossaryTerm :GlossaryTerm ) {
    const dialogRef = this.dialog.open(DialogComponent, {
      width: '600px',
      data:{ glossaryTerm, action},
      disableClose: true 
    });
    dialogRef.afterClosed().subscribe(result => {
      if(result.event!== 'Cancel'){
      this.getGlossaryTerms();
      }
    });
  }
}
