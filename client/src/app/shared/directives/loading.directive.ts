import { Directive, HostBinding, inject, Input, OnChanges, SimpleChanges } from '@angular/core';

import { LoaderService } from "../services/loader.service";

@Directive({
  selector: '[appLoading]'
})
export class LoadingDirective implements OnChanges {
  @Input('appLoading') isLoading: boolean | null = false;
  @HostBinding('style.cursor') get cursor() {return this.isLoading ? 'none' : 'auto';}

  readonly loaderService = inject(LoaderService);

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['appLoading']) {
      const isLoading = changes['appLoading'].currentValue;
      this.loaderService.setLoadingState(isLoading);
    }
  }
}
