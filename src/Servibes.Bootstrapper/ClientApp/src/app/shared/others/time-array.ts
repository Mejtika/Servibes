import { Injectable } from '@angular/core';

@Injectable()
export class TimeArray {

    generateHours(minutesInterval:number = 15): string[] {

        var times = [];
        var startTime = 0;
        
        for (var i = 0; startTime < 24*60; i++) {
            var hour = Math.floor(startTime / 60);
            var minutes = (startTime % 60);
            times[i] = ("0" + (hour % 24)).slice(-2) + ':' + ("0" + minutes).slice(-2);
            startTime = startTime + minutesInterval;
        }

        return times;
    }

    generateMinutes(minutesInterval:number = 15, startTime:number = 0, endTime:number = 60): string[] {
        var times = [];
        times[0] = startTime;

        for(var i = 1; startTime < endTime; i++) {
            startTime += minutesInterval;
            times[i] = startTime;
        }

        return times;
    }
    
}

