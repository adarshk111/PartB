import { TestBed } from '@angular/core/testing';

import { GlossaryTermsService } from './glossary-terms.service';

describe('GlossaryTermsService', () => {
  let service: GlossaryTermsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GlossaryTermsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
