export interface MakeTimeReservationRequest {
    start: string;
    duration: number;
}

export interface GiveTimeOffRequest {
    start: string;
    end: string;
}

export interface CancelTimeOffRequest {
    start: string;
}