import { platformBrowserDynamic } from "@angular/platform-browser-dynamic";
import { bootloader } from "@angularclass/hmr";
import { AppModule } from "./app/boot";

export function main(): Promise<any> {
    return platformBrowserDynamic()
        .bootstrapModule(AppModule)
        .catch((err) => console.error(err));
}

bootloader(main);