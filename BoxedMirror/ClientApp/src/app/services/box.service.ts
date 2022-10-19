import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export class BoxService {
	http: HttpClient;
	baseUrl: string;

	constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
		this.http = http;
		this.baseUrl = baseUrl;
	}

	// Returns an observable
	processLaser(boxInput: BoxInput): Observable<any> {
		return this.http.post<Room>(this.baseUrl + 'laser/process', boxInput);
	}
}
