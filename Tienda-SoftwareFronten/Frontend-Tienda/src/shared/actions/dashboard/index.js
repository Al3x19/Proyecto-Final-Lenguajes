import { tiendaApi } from "../../../config/api";

export const dashboardAsync = async () => {
    try {
        const { data } = await tiendaApi.get("/dashboard/info");
        return data;
    } catch (error) {
        console.error(error);
        return error?.response.data;
    }
}