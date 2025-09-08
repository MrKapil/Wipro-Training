import 'zone.js';
import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors, withFetch } from '@angular/common/http';
import { importProvidersFrom } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppComponent } from './app/app.component'
import { APP_ROUTES } from './app/app.routes';
import { authInterceptor } from './app/core/interceptors/auth-interceptor';
import { errorInterceptor } from './app/core/interceptors/error-interceptor';

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(APP_ROUTES),
    provideHttpClient(
      withFetch(),
      withInterceptors([authInterceptor, errorInterceptor])
    ),
    importProvidersFrom(BrowserAnimationsModule),
    
  ]
}).catch(err => console.error(err));



// import { bootstrapApplication } from '@angular/platform-browser';
// import { AppComponent } from './app/app.component';
// import { appConfig } from './app/app.config';

// bootstrapApplication(AppComponent, appConfig)
//   .catch((err) => console.error(err));
