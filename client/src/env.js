// @ts-ignore
export const dev = window.location.origin.includes('localhost')

// NOTE don't forget to change your baseURL if using the dotnet template
export const baseURL = dev ? 'https://localhost:7045' : ''
export const useSockets = false

// TODO change these variables out to your own auth after cloning!
export const domain = 'dev-rl8038aaudw3trob.us.auth0.com'
export const clientId = 'H0dqgRYCRdtyN24QvvlyPuo2FZahpeOp'
export const audience = 'https://whimsicalsnail.com/'