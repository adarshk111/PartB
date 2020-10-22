import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MockComponent } from 'ng-mocks';
import { TermDefinitionDisplayComponent } from './term-definition-display.component';
import { GlossaryTermsService } from '../services/glossary-terms.service';
import { DialogComponent } from '../dialog/dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { HttpClientModule } from '@angular/common/http';
import { of } from 'rxjs';
import { GlossaryTerm } from '../models/GlossaryTerm';
import { MatTableModule } from '@angular/material/table';

describe('TermDefinitionDisplayComponent', () => {
  let component: TermDefinitionDisplayComponent;
  let fixture: ComponentFixture<TermDefinitionDisplayComponent>;
  let glossaryTermsService : GlossaryTermsService;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [MatDialogModule, HttpClientModule, MatTableModule],
      declarations: [ TermDefinitionDisplayComponent, MockComponent(DialogComponent) ],
      providers :[GlossaryTermsService]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    glossaryTermsService = TestBed.get(GlossaryTermsService);
    fixture = TestBed.createComponent(TermDefinitionDisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  fit('ngOnInit should get GlossaryTermDetails from GlossaryTermService', () => {
    var glossaryTerms : GlossaryTerm[] = [
      {
        id : 1,
        term : 'someterm',
        definition : 'newmeaning'
      },
      {
        id : 2,
        term : 'someterm2',
        definition : 'newmeaning2'
      }
    ]
    spyOn(glossaryTermsService, 'getGlossaryTerms').and.returnValue(of(glossaryTerms));
    component.ngOnInit();
    expect(component.glossaryTerms).toEqual(glossaryTerms);
  });
});
