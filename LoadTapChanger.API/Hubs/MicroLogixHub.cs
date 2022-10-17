using Microsoft.AspNetCore.SignalR;
namespace LoadTapChanger.API.Hubs;

public class MicroLogixHub : Hub
{
    /* This hub is used to establish a connection to the MicroLogix PLC
     * and to send and receive data from it.
     * The client will connect to this hub and pass a Tag's name to the hub.
     * The hub will then subscribe to the Tag's value change event and
     * send the new value to the client when it changes.
     * 
     * A client should be able to invoke the following methods:
     *  - InitializeConnection(string connectionString)
     *      - This method will be called when the client connects to the hub.
     *      - No other methods should be called until this method has been called.
     *  - MonitorTagAsync(string tagName)
     *      - This method will be called when the client wants to monitor a Tag.
     *  - DisconnectFromTag(string tagName)
     *  - WriteTagAsync(string tagName, object value)
     *  - ReadTagAsync(string tagName)
     *  - DisposeConnection()
     *      - This method will be called when the client disconnects from the hub.
     *      - No other methods should be called after this method has been called.
     * This Hub will also need to allow the client to invoke multiple MonitorTagAsync methods
     * so it can be notified when each Tag's value changes.
     * 
     * Client shoudld be able to pass a list of tags to the hub and the hub should
     *  - Subscribe to each Tag's value change event
     *  - Send the new value to the client when it changes
     *  - Unsubscribe from each Tag's value change event when the client disconnects
     *  
     * The client should be able to pass a Tag's name to the hub and the hub should
     *
     */


    public MicroLogixHub()
    {

    }

    /* Copilot Attempt - Comment
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception) => await base.OnDisconnectedAsync(exception);

    public async Task InitializeConnection(string connectionString)
    {

    }

    public async Task MonitorTagAsync(string tagName)
    {

    }

    public async Task DisconnectFromTag(string tagName)
    {

    }

    public async Task WriteTagAsync(string tagName,object value)
    {

    }

    public async Task ReadTagAsync(string tagName)
    {

    }

    public async Task DisposeConnection()
    {

    }
    */




}

