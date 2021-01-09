export interface TimeReservation {
    timeReservationId: string;
    status: string;
    employeeId: string;
    start: Date;
    end: Date;
}

export interface Appointment {
    appointmentId: string;
    status: string;
    employeeId: string;
    employeeName: string;
    reserveeName: string;
    serviceName: string;
    servicePrice: number;
    start: Date;
    end: Date;
}

export interface CompanyTimeOff {
    employeeId: string;
    start: Date;
    end: Date;
}