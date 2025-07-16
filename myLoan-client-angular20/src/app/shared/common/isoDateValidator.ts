import { AbstractControl, ValidationErrors } from '@angular/forms';

export function isoDateValidator(control: AbstractControl): ValidationErrors | null {
  const value = control.value;

  // Allow empty if field is optional
  if (!value) return null;

  // Validate ISO 8601 date (YYYY-MM-DD or full ISO string)
  const isoDatePattern = /^\d{4}-\d{2}-\d{2}$/; // for date only

  const isPatternValid = isoDatePattern.test(value);
  const date = new Date(value);
  const isDateValid = !isNaN(date.getTime());

  const isValid = isPatternValid && isDateValid;

  return isValid ? null : { isoDate: true };
}
