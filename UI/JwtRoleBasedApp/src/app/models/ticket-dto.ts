export interface TicketDTO {
    ticketId: number;
    numberOfSeats: number;
    bookingDate: Date;
    userId: string;
    showId: number;
    movieName?: string;
    showTime?: number;
    screenNumber?: number;
  }