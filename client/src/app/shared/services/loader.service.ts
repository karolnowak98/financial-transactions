import { Injectable } from '@angular/core';
import { Observable, Subject} from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LoaderService {
  isLoadingSubject = new Subject<boolean>();

  public loadingState$ : Observable<boolean> = this.isLoadingSubject.asObservable();

  setLoadingState(isLoading: boolean): void {
    this.isLoadingSubject.next(isLoading);
    if (isLoading) {
      document.body.classList.add('loading-cursor');
    } else {
      document.body.classList.remove('loading-cursor');
    }
  }
}
