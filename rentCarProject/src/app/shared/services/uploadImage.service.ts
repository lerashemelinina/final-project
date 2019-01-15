import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class UploadImageService {

  constructor(private http : HttpClient) { }

  postFile(imageFolder: string, fileToUpload: File) {
    const serverUrl = 'http://localhost:63174/api/UploadImage'
    const formData: FormData = new FormData();
    formData.append('Image', fileToUpload, fileToUpload.name);
    formData.append('ImageFolder', imageFolder);
    return this.http.post(serverUrl, formData);
  }

}