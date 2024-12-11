import { create } from "zustand";
import { dashboardAsync } from "../../shared/actions/dashboard";

export const useDashboardStore = create((set) => ({
    dashboardData: {
        listsCount: 0,
        tagsCount: 0,
        softwareCount: 0,
        reviewsCount: 0,
        softwares: [],
        users: [],
        tags: [],
        reviews: []
    },
    loadData: async () => {
        const result = await dashboardAsync();

        if(result.status) {
            set({dashboardData: result.data});
            return;
        }

        set({dashboardData: null});
        return;
    }
}));