import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';

@Injectable()
export class DetailServcies {
    constructor(private http: Http) {

    }
    getDetailList() {
        return this.http.get('http://localhost:55100/api/detail');        
    }
    getKeeperList() {
        return this.http.get('http://localhost:55100/api/keepers');
    }
    getKeeperCountList() {
        return this.http.get('http://localhost:55100/api/keeperlist');
    }
    removeKeeper(kepId: any) {
        let headers = new Headers({
            'Content-Type':
            'application/json; charset=utf-8'
        });
        return this.http.delete('http://localhost:55100/api/keeperlist', new RequestOptions({
            headers: headers,
            body: kepId
        }));
    }
    postData(detObj: any) {
        let headers = new Headers({
            'Content-Type':
            'application/json; charset=utf-8'
        });
        let options = new RequestOptions({ headers: headers}); 
        return this.http.post('http://localhost:55100/api/detail', JSON.stringify(detObj), options);
    }
    postDataKeeper(kepObj: any) {
        let headers = new Headers({
            'Content-Type':
            'application/json; charset=utf-8'
        });
        let options = new RequestOptions({ headers: headers });
        return this.http.post('http://localhost:55100/api/keepers', JSON.stringify(kepObj), options);
    }
    removeDetailOnDB(detId: any) {
        let headers = new Headers({
            'Content-Type':
            'application/json; charset=utf-8'
        });
        return this.http.delete('http://localhost:55100/api/detail', new RequestOptions({
            headers: headers,
            body: detId
        }));
    }
    removeDetail(uptId: any) {
        let headers = new Headers({
            'Content-Type':
            'application/json; charset=utf-8'
        });
        let options = new RequestOptions({ headers: headers });
        return this.http.put('http://localhost:55100/api/detail', JSON.stringify(uptId), options)
    }
} 