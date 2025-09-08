import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '@env/environment';
import { CommonModule, JsonPipe } from '@angular/common';

@Component({
  selector: 'app-bulk-upload',
  standalone: true,
  imports: [CommonModule, JsonPipe],
  templateUrl: './bulkupload.html',
  styleUrls: ['./bulkupload.css']
})
export class BulkUploadComponent {
  file?: File;
  result: any;
  constructor(private http: HttpClient){}

  onFile(event: any) { this.file = event.target.files[0]; }

  upload() {
    if (!this.file) return alert('Select a file');
    const fd = new FormData(); fd.append('file', this.file);
    this.http.post(`${environment.apiUrl}/BulkUpload/products`, fd).subscribe(res => {
      this.result = res; alert('Upload complete');
    }, err => alert('Upload failed'));
  }
}
