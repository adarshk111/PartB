import { Component, Inject, Optional, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { GlossaryTerm } from '../models/GlossaryTerm';
import { GlossaryTermsService } from '../services/glossary-terms.service';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.css']
})
export class DialogComponent implements OnInit {

  action:string;
  glossaryTerm:GlossaryTerm = new GlossaryTerm();
  termFormControl = new FormControl();
  definitionFormControl = new FormControl();
  constructor(public dialogRef: MatDialogRef<DialogComponent>,
  @Optional() @Inject(MAT_DIALOG_DATA) public data: any,
  private glossaryTermsService: GlossaryTermsService ) {   
  }

  ngOnInit(): void {
    console.log(this.data)
    this.termFormControl.setValue(this.data.glossaryTerm.term);
    this.definitionFormControl.setValue(this.data.glossaryTerm.definition);
    this.glossaryTerm = this.data.glossaryTerm;
    this.action = this.data.action;
     }
  doAction(){
    this.glossaryTerm.term = this.termFormControl.value;
    this.glossaryTerm.definition = this.definitionFormControl.value;
    console.log(this.action)
    if(this.action === 'Create')
    {
      this.CreateGlossaryItem();
    }
    if(this.action === 'Update')
    {
      this.UpdateGlossaryItem();
    }
    if(this.action === 'Delete')
    {
      this.DeleteGlossaryItem();
    }
  }
  isAddDisabled(): boolean {
    return (this.definitionFormControl.value === undefined ||this.definitionFormControl.value === '' ||
      this.termFormControl.value === undefined  || this.termFormControl.value === '');
  }
  UpdateGlossaryItem() {
    this.glossaryTermsService.UpdateGlossaryTerm(this.glossaryTerm).subscribe((res) => {
      this.dialogRef.close({event: 'Update'});
    });
  }
  DeleteGlossaryItem() {
    this.glossaryTermsService.DeleteGlossaryTerm(this.glossaryTerm.id).subscribe((res) => {
      this.dialogRef.close({event: 'Delete'});
    });
  }
  CreateGlossaryItem() {
    console.log(this.glossaryTermsService)
    this.glossaryTermsService.CreateGlossaryTerm(this.glossaryTerm).subscribe((res) => {
      this.dialogRef.close({event: 'Create'});
    });
  }

  closeDialog(){
    this.dialogRef.close({event: 'Cancel'});
  }
}
