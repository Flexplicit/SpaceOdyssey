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
  let seconds = (ms / 1000).toFixed(0)
  let minutes = (ms / (1000 * 60)).toFixed(0)
  let hours = (ms / (1000 * 60 * 60)).toFixed(0)
  let days = (ms / (1000 * 60 * 60 * 24)).toFixed(0)
  if (parseInt(seconds) < 60) return seconds + ' Sec'
  else if (parseInt(minutes) < 60) return minutes + ' Min'
  else if (parseInt(hours) < 24) return hours + ' Hrs'
  else return days + ' Days'
}
