export class Constants{
    public static get Host(): string { return "https://localhost:5001/"}
    public static get ChatHubUrl(): string {return this.Host + "chatHub"}

}

export class SignalRMethods {
    public static get SendDM(): string {return "SendDirectMessage"}
    public static get SendToAll() : string {return "SendMessageToAll"}
    public static get ReceiveMessage() : string {return "RecieveMessage"}
}
