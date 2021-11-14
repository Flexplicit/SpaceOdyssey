export const convertStringToDateString = (dateStr: string): string => {
  return dateStr === '' ? '' : new Date(dateStr).toLocaleString([], { day: '2-digit', month: '2-digit', year: '2-digit', hour: '2-digit', minute: '2-digit' })
}

export const convertStringToHoursMinutesString = (dateStr: string): string => {
  return dateStr === '' ? '' : new Date(dateStr).toLocaleString([], { hour: '2-digit', minute: '2-digit', hourCycle: 'h24' })
}

export const convertStringToDateAndHours = (dateStr: string): string => {
  return dateStr === '' ? '' : new Date(dateStr).toLocaleString([], { day: '2-digit', month: '2-digit', hour: '2-digit', minute: '2-digit', hourCycle: 'h24' })
}

export const getHumanReadableTimeFromHours = (hours: number) => {
  return getHumanReadableTimeFromMilliseconds(hours * 3600000)
}

export const getHumanReadableTimeFromMilliseconds = (ms: number) => {
  let seconds = Math.floor(ms / 1000).toFixed(0)
  let minutes = Math.floor(ms / (1000 * 60)).toFixed(0)
  let hours = Math.floor(ms / (1000 * 60 * 60)).toFixed(0)
  let days = Math.floor(ms / (1000 * 60 * 60 * 24)).toFixed(0)
  if (parseInt(seconds) < 60) return `${seconds} Sec`
  else if (parseInt(minutes) < 60) return `${minutes} Mins`
  else if (parseInt(hours) < 24) return `${hours} Hrs`
  else return `${days} Days ${parseInt(hours) % 24} Hrs`
}

export const getHoursBetweenDates = (start: Date, end: Date) => {
  return Math.floor(Math.abs(end.getTime() - start.getTime())) / 36e5
}

export const getHumanReadableTimeFromDifferenceBetweenDates = (startDate: string, endDate: string): string => {
  let ms = new Date(endDate).getTime() - new Date(startDate).getTime()
  let seconds = (ms / 1000).toFixed(0)
  let minutes = (ms / (1000 * 60)).toFixed(0)
  let hours = (ms / (1000 * 60 * 60)).toFixed(0)
  let days = (ms / (1000 * 60 * 60 * 24)).toFixed(0)

  if (parseInt(seconds) < 60) return `${seconds} Sec`
  else if (parseInt(minutes) < 60) return `${minutes} Mins`
  else if (parseInt(hours) < 24) return `${hours} Hrs ${parseInt(minutes) % 60} mins `
  else return `${days} Days ${parseInt(hours) % 24} Hrs ${parseInt(minutes) % 60} mins `
}

export const getTomorrowsDate = (): Date => {
  let curr = new Date()
  curr.setDate(curr.getDate() + 1)
  curr.setHours(0)
  curr.setMinutes(0)
  curr.setMilliseconds(0)
  return curr
}
