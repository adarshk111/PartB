import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MockComponent } from 'ng-mocks';
import { DialogComponent } from './dialog.component';
import { GlossaryTermsService } from '../services/glossary-terms.service';
import { HttpClientModule } from '@angular/common/http';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDialogRef, MatDialogModule,MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TermDefinitionDisplayComponent } from '../term-definition-display/term-definition-display.component';
import { of } from 'rxjs';
describe('DialogComponent', () => {
  let component: DialogComponent;
  let fixture: ComponentFixture<DialogComponent>;
  let glossaryTermsService : GlossaryTermsService;
  let glossaryTerm = {
    id : 1,
    term : 'someterm',
    definition : 'newmeaning'
  };
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule, MatFormFieldModule, MatDialogModule, ReactiveFormsModule, MatInputModule, BrowserAnimationsModule],
      declarations: [ DialogComponent, MockComponent(TermDefinitionDisplayComponent) ],
      providers :[
        GlossaryTermsService,
        { provide: MatDialogRef, useValue: {} },
        {provide: MAT_DIALOG_DATA, useValue: {  glossaryTerm, action: 'Create'  }}
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    glossaryTermsService = TestBed.get(GlossaryTermsService);
    spyOn(glossaryTermsService,'CreateGlossaryTerm').and.returnValue(of(glossaryTerm));
    spyOn(glossaryTermsService,'DeleteGlossaryTerm').and.returnValue(of(glossaryTerm));
    spyOn(glossaryTermsService,'UpdateGlossaryTerm').and.returnValue(of(glossaryTerm));
    fixture = TestBed.createComponent(DialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  fit('should create glossary item when action is create', () => {
    component.doAction();
    expect(glossaryTermsService.CreateGlossaryTerm).toHaveBeenCalledTimes(1);
  });
  fit('should update glossary item when action is update', () => {
  component.action = 'Update';
    component.doAction();
    expect(glossaryTermsService.UpdateGlossaryTerm).toHaveBeenCalledTimes(1);
  });
});
