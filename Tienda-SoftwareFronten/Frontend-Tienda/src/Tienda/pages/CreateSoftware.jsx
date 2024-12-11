import React from "react";
import { Formik, Form, Field, ErrorMessage } from "formik";
import { softwareValidationSchema } from "../forms/software.data";

const SoftwareFormPage = ({ onSubmit }) => {
  const initialValues = {
    Name: "",
    Description: "",
    Price: "",
    Icon: "",
    Version: "",
    TagList: [],
  };

  return (
    <main className="flex flex-col items-center justify-center p-8 space-y-6 md:p-16 bg-gray-100 w-full">
      <Formik
        initialValues={initialValues}
        validationSchema={softwareValidationSchema}
        onSubmit={(values) => {
          console.log(values);
          onSubmit(values);
        }}
      >
        <Form className="space-y-4 w-full max-w-2xl bg-white rounded-lg shadow-lg p-6">
          <div>
            <label>Nombre</label>
            <Field name="Name" type="text" className="input-field" />
            <ErrorMessage name="Name" component="div" className="error-message" />
          </div>

          <div>
            <label>Descripción</label>
            <Field name="Description" as="textarea" className="input-field" />
            <ErrorMessage name="Description" component="div" className="error-message" />
          </div>

          <div>
            <label>Precio</label>
            <Field name="Price" type="number" className="input-field" />
            <ErrorMessage name="Price" component="div" className="error-message" />
          </div>

          <div>
            <label>Icono</label>
            <Field name="Icon" type="text" className="input-field" placeholder="Ingrese una URL válida" />
            <ErrorMessage name="Icon" component="div" className="error-message" />
          </div>

          <div>
            <label>Versión</label>
            <Field name="Version" type="text" className="input-field" />
            <ErrorMessage name="Version" component="div" className="error-message" />
          </div>

          <div>
            <label>Tags</label>
            <Field name="TagList" type="text" className="input-field" placeholder="Comma-separated tags" />
            <ErrorMessage name="TagList" component="div" className="error-message" />
          </div>

          <button type="submit" className="submit-button">
            Guardar
          </button>
        </Form>
      </Formik>
    </main>
  );
};

export default SoftwareFormPage;
