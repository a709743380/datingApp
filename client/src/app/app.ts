import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.css'
})

export class App implements OnInit {
  public readonly title = "Dating App";
  //注入依賴
  private http = inject(HttpClient);
  private url = "https://localhost:5001/api/members/";
  public members: any = signal<any>([]);

  //初始化
  async ngOnInit() {
    this.members.set(await this.getMembers())
  }
  async getMembers() {
    try {
      return lastValueFrom(this.http.get(this.url))
    } catch (error) {
      console.log(error)
      throw (error)
    }
  }
}
