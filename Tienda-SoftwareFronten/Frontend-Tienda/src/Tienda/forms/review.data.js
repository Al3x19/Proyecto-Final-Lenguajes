import * as Yup from 'yup';
export const reviewInitValues = {
    score: 0,
    content: '',
    SoftwareId: ""
}
export const reviewValidationSchema = Yup.object({
    score: Yup.number()
        .required('La valoracion es necesaria.')
        .min(0, 'La valoración debe ser al menos 0.')
        .max(100, 'La valoración no puede ser mayor a 100.'),
    content: Yup.string()
        .required('El contenido es requerido.')
        .min(10, 'El contenido debe tener al menos 10 caracteres.')
})