import { EventEmitter, Output } from '@angular/core';
import {
  ChangeDetectorRef,
  Component,
  Inject,
  Injectable,
  Input,
  LOCALE_ID,
  OnInit,
} from "@angular/core";
import {
  CalendarUtils,
  CalendarWeekViewComponent,
  DateAdapter,
  getWeekViewPeriod,
} from "angular-calendar";
import {
  WeekView,
  GetWeekViewArgs,
  EventColor,
  CalendarEvent,
} from "calendar-utils";

export interface User {
  id: string;
  name: string;
  color: EventColor;
}

interface DayViewScheduler extends WeekView {
  users: User[];
}

interface GetWeekViewArgsWithUsers extends GetWeekViewArgs {
  users: User[];
}

@Injectable()
export class DayViewSchedulerCalendarUtils extends CalendarUtils {
  getWeekView(args: GetWeekViewArgsWithUsers): DayViewScheduler {
    const { period } = super.getWeekView(args);
    const view: DayViewScheduler = {
      period,
      allDayEventRows: [],
      hourColumns: [],
      users: [...args.users],
    };

    view.users.forEach((user, columnIndex) => {
      const events = args.events.filter(
        (event) => event.meta.user.id === user.id
      );
      const columnView = super.getWeekView({
        ...args,
        events,
      });
      view.hourColumns.push(columnView.hourColumns[0]);
      columnView.allDayEventRows.forEach(({ row }, rowIndex) => {
        view.allDayEventRows[rowIndex] = view.allDayEventRows[rowIndex] || {
          row: [],
        };
        view.allDayEventRows[rowIndex].row.push({
          ...row[0],
          offset: columnIndex,
          span: 1,
        });
      });
    });

    return view;
  }
}

@Component({
  selector: "mwl-day-view-scheduler",
  templateUrl: "./day-view-scheduler.component.html",
  providers: [DayViewSchedulerCalendarUtils]
})
export class DayViewSchedulerComponent extends CalendarWeekViewComponent {
  @Input() users: User[] = [];
  @Input() events: CalendarEvent[] = [];
  @Output() clickedEvent: EventEmitter<CalendarEvent> = new EventEmitter<CalendarEvent>();
  view: DayViewScheduler;
  daysInWeek = 1;

  constructor(
    protected cdr: ChangeDetectorRef,
    protected utils: DayViewSchedulerCalendarUtils,
    @Inject(LOCALE_ID) locale: string,
    protected dateAdapter: DateAdapter
  ) {
    super(cdr, utils, locale, dateAdapter);
  }

  trackByUserId = (index: number, row: User) => row.id;

  handleEventClick(event: CalendarEvent){
    this.clickedEvent.emit(event);
  }
  
  protected getWeekView(events: CalendarEvent[]) {
    return this.utils.getWeekView({
      events,
      users: this.users,
      viewDate: this.viewDate,
      weekStartsOn: this.weekStartsOn,
      excluded: this.excludeDays,
      precision: this.precision,
      absolutePositionedEvents: true,
      hourSegments: this.hourSegments,
      dayStart: {
        hour: this.dayStartHour,
        minute: this.dayStartMinute,
      },
      dayEnd: {
        hour: this.dayEndHour,
        minute: this.dayEndMinute,
      },
      segmentHeight: this.hourSegmentHeight,
      weekendDays: this.weekendDays,
      ...getWeekViewPeriod(
        this.dateAdapter,
        this.viewDate,
        this.weekStartsOn,
        this.excludeDays,
        this.daysInWeek
      ),
    });
  }
}
