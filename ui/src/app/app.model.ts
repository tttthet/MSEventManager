export class Event
{
  title: string;
  datetime: string;
  attendeed: any[];
}

export class User
{
  id: number;
  name: string;
}

export class Invitation
{
  eventId: number;
  title: string;
}
