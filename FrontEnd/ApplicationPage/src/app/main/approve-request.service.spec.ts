import { TestBed } from '@angular/core/testing';

import { ApproveRequestService } from './approve-request.service';

describe('AproveRequestService', () => {
  let service: ApproveRequestService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApproveRequestService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
