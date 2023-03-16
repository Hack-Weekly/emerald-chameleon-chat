import { NextApiRequest, NextApiResponse } from 'next'

export interface GetHubUrlReq {
  data: {
    chatRoomId: string
  }
}

export interface GetHubUrlReq extends NextApiRequest {
  body: GetHubUrlReq
}

export interface GetHubUrlRes {
  data: {
    hubUrl: string
  }
}

export interface GetHubUrlRes extends NextApiResponse {
  body: GetHubUrlRes
}

const handler = async (req: GetHubUrlReq, res: GetHubUrlRes) => {
  const chatRoomId = req.body.data.chatRoomId
  console.log('chatRoomId: ', chatRoomId)
  //Here will be the method to fetch the hubUrl from the backend using the chatRoomId
  // res.status(200).json({ data: { hubUrl } })

  res.status(200).json({ data: { hubUrl: 'No' } })
}

export default handler
