'use client'

import { useEffect, useRef } from 'react'
import {
  HubConnection,
  HubConnectionBuilder,
  HubConnectionState,
  LogLevel,
} from '@microsoft/signalr'

export interface SignalRConnectionOptions {
  logLevel?: LogLevel
  transportType?: 'webSockets' | 'serverSentEvents' | 'longPolling'
  accessTokenFactory?: () => string | Promise<string>
}

// Custom react hook that returns the reference to the SignalR connection
// https://www.roundthecode.com/dotnet/signalr/integrating-signalr-with-react-typescript-and-asp-net-core
// https://github.com/dotnet/aspnetcore/issues/46708

const useSignalR = (hubName: string, options?: SignalRConnectionOptions) => {
  const connectionRef = useRef<HubConnection>()

  useEffect(() => {
    if (hubName === '') return
    const connection = new HubConnectionBuilder()
      .withUrl(`/hubs/${hubName}`, options || {})
      .build()

    connectionRef.current = connection

    return () => {
      connection.stop()
    }
  }, [hubName, options])

  useEffect(() => {
    if (hubName === '') return
    const connection = connectionRef.current

    if (connection?.state === HubConnectionState.Disconnected) {
      connection.start().catch(console.error)
    }
  }, [])

  return connectionRef.current
}

export default useSignalR
